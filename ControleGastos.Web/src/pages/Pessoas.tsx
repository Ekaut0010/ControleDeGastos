import { useEffect, useState } from "react";
import { api } from "../services/api";
import type { Pessoa, CriarPessoa } from "../types"; // <- Adicionado 'type'
export function Pessoas() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState<number | "">("");

  const carregarPessoas = async () => {
    try {
      const response = await api.get<Pessoa[]>("/pessoas");
      setPessoas(response.data);
    } catch (err) {
      console.error("Erro ao carregar pessoas:", err);
    }
  };

  useEffect(() => {
    carregarPessoas();
  }, []);

  const handleCadastrar = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!nome || idade === "") return;

    try {
      const novaPessoa: CriarPessoa = { nome, idade: Number(idade) };
      await api.post("/pessoas", novaPessoa);
      setNome("");
      setIdade("");
      carregarPessoas();
    } catch (err) {
      alert("Erro ao cadastrar pessoa.");
    }
  };

  const handleExcluir = async (id: number) => {
    if (
      !confirm(
        "Deseja excluir esta pessoa? Todas as transações dela serão apagadas.",
      )
    )
      return;

    try {
      await api.delete(`/pessoas/${id}`);
      carregarPessoas();
    } catch (err) {
      alert("Erro ao excluir pessoa.");
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h2>Cadastro de Pessoas</h2>

      <form
        onSubmit={handleCadastrar}
        style={{ display: "flex", gap: "10px", marginBottom: "20px" }}
      >
        <input
          type="text"
          placeholder="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
        />
        <input
          type="number"
          placeholder="Idade"
          value={idade}
          onChange={(e) =>
            setIdade(e.target.value === "" ? "" : Number(e.target.value))
          }
          required
          min="0"
        />
        <button type="submit">Cadastrar</button>
      </form>

      <table
        border={1}
        cellPadding={8}
        style={{ borderCollapse: "collapse", width: "100%" }}
      >
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Idade</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map((p) => (
            <tr key={p.id}>
              <td>{p.id}</td>
              <td>{p.nome}</td>
              <td>{p.idade} anos</td>
              <td>
                <button onClick={() => handleExcluir(p.id)}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
