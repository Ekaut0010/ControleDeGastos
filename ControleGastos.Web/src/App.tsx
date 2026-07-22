import { useState } from "react";
import { Pessoas } from "./pages/Pessoas";
import { Transacoes } from "./pages/Transacoes";
import { Totais } from "./pages/Totais";

export function App() {
  const [abaAtiva, setAbaAtiva] = useState<"pessoas" | "transacoes" | "totais">(
    "pessoas",
  );

  return (
    <div>
      <header>
        {/* Título sem 'Categoria:' */}
        <h1>Controle de Gastos Residenciais</h1>

        {/* Botões sem 'Categoria:' */}
        <nav>
          <button
            className={abaAtiva === "pessoas" ? "active" : ""}
            onClick={() => setAbaAtiva("pessoas")}
          >
            Pessoas
          </button>

          <button
            className={abaAtiva === "transacoes" ? "active" : ""}
            onClick={() => setAbaAtiva("transacoes")}
          >
            Transações
          </button>

          <button
            className={abaAtiva === "totais" ? "active" : ""}
            onClick={() => setAbaAtiva("totais")}
          >
            Totais
          </button>
        </nav>
      </header>

      <main>
        {abaAtiva === "pessoas" && <Pessoas />}
        {abaAtiva === "transacoes" && <Transacoes />}
        {abaAtiva === "totais" && <Totais />}
      </main>
    </div>
  );
}

export default App;
