import { useEffect, useState } from "react";
import { api } from "../services/api";
import type { TotaisResponse } from "../types"; // <- Adicionado 'type'
export function Totais() {
  const [totais, setTotais] = useState<TotaisResponse | null>(null);

  useEffect(() => {
    api
      .get<TotaisResponse>("/totais")
      .then((res) => setTotais(res.data))
      .catch((err) => console.error("Erro ao buscar totais:", err));
  }, []);

  if (!totais)
    return <div style={{ padding: "20px" }}>Carregando dados...</div>;

  return (
    <div style={{ padding: "20px" }}>
      <h2>Consulta de Totais</h2>

      <table
        border={1}
        cellPadding={8}
        style={{ borderCollapse: "collapse", width: "100%" }}
      >
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Total Receitas</th>
            <th>Total Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {totais.pessoas.map((p) => (
            <tr key={p.id}>
              <td>{p.nome}</td>
              <td style={{ color: "green" }}>
                R$ {p.totalReceitas.toFixed(2)}
              </td>
              <td style={{ color: "red" }}>R$ {p.totalDespesas.toFixed(2)}</td>
              <td
                style={{
                  fontWeight: "bold",
                  color: p.saldo >= 0 ? "blue" : "red",
                }}
              >
                R$ {p.saldo.toFixed(2)}
              </td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr style={{ backgroundColor: "#f0f0f0", fontWeight: "bold" }}>
            <td>TOTAL GERAL</td>
            <td style={{ color: "green" }}>
              R$ {totais.totalReceitas.toFixed(2)}
            </td>
            <td style={{ color: "red" }}>
              R$ {totais.totalDespesas.toFixed(2)}
            </td>
            <td style={{ color: totais.saldo >= 0 ? "blue" : "red" }}>
              R$ {totais.saldo.toFixed(2)}
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  );
}
