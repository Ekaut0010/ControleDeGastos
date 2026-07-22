import { useEffect, useState } from "react";
import { api } from "../services/api";
import { TipoTransacao } from "../types";
import type { Pessoa, Transacao, CriarTransacao } from "../types"; // <- Tipos separados com 'type'

export function Transacoes() {
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState<number | "">("");
  const [tipo, setTipo] = useState<TipoTransacao>(TipoTransacao.Despesa);
  const [pessoaId, setPessoaId] = useState<number | "">("");

  const pessoaSelecionada = pessoas.find((p) => p.id === Number(pessoaId));
  const ehMenorDeIdade = pessoaSelecionada
    ? pessoaSelecionada.idade < 18
    : false;

  const carregarDados = async () => {
    try {
      const [resTransacoes, resPessoas] = await Promise.all([
        api.get<Transacao[]>("/transacoes"),
        api.get<Pessoa[]>("/pessoas"),
      ]);
      setTransacoes(resTransacoes.data);
      setPessoas(resPessoas.data);
    } catch (err) {
      console.error("Erro ao carregar dados:", err);
    }
  };

  useEffect(() => {
    carregarDados();
  }, []);

  // Se for menor de 18 anos, força o tipo para Despesa
  useEffect(() => {
    if (ehMenorDeIdade) {
      setTipo(TipoTransacao.Despesa);
    }
  }, [pessoaId, ehMenorDeIdade]);

  const handleCadastrar = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!descricao || valor === "" || pessoaId === "") return;

    try {
      const novaTransacao: CriarTransacao = {
        descricao,
        valor: Number(valor),
        tipo,
        pessoaId: Number(pessoaId),
      };

      await api.post("/transacoes", novaTransacao);
      setDescricao("");
      setValor("");
      carregarDados();
    } catch (err: any) {
      alert(err.response?.data?.mensagem || "Erro ao cadastrar transação.");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h2>Cadastro de Transações</h2>

      <form
        onSubmit={handleCadastrar}
        style={{
          display: "flex",
          flexDirection: "column",
          gap: "10px",
          maxWidth: "400px",
          marginBottom: "20px",
        }}
      >
        <input
          type="text"
          placeholder="Descrição"
          value={descricao}
          onChange={(e) => setDescricao(e.target.value)}
          required
        />
        <input
          type="number"
          step="0.01"
          placeholder="Valor (R$)"
          value={valor}
          onChange={(e) =>
            setValor(e.target.value === "" ? "" : Number(e.target.value))
          }
          required
          min="0.01"
        />

        <select
          value={pessoaId}
          onChange={(e) => setPessoaId(Number(e.target.value))}
          required
        >
          <option value="">Selecione uma pessoa...</option>
          {pessoas.map((p) => (
            <option key={p.id} value={p.id}>
              {p.nome} ({p.idade} anos)
            </option>
          ))}
        </select>

        <select
          value={tipo}
          onChange={(e) => setTipo(Number(e.target.value) as TipoTransacao)}
        >
          <option value={TipoTransacao.Despesa}>Despesa</option>
          <option value={TipoTransacao.Receita} disabled={ehMenorDeIdade}>
            Receita {ehMenorDeIdade ? "(Indisponível para menores)" : ""}
          </option>
        </select>

        <button type="submit">Cadastrar Transação</button>
      </form>

      <table
        border={1}
        cellPadding={8}
        style={{ borderCollapse: "collapse", width: "100%" }}
      >
        <thead>
          <tr>
            <th>ID</th>
            <th>Descrição</th>
            <th>Valor</th>
            <th>Tipo</th>
            <th>Pessoa</th>
          </tr>
        </thead>
        <tbody>
          {transacoes.map((t) => (
            <tr key={t.id}>
              <td>{t.id}</td>
              <td>{t.descricao}</td>
              <td>R$ {t.valor.toFixed(2)}</td>
              <td>
                {t.tipo === TipoTransacao.Receita ? "Receita" : "Despesa"}
              </td>
              <td>{t.pessoa?.nome || `Pessoa ID: ${t.pessoaId}`}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
