export const TipoTransacao = {
  Despesa: 0,
  Receita: 1,
} as const;

export type TipoTransacao = (typeof TipoTransacao)[keyof typeof TipoTransacao];

export interface Pessoa {
  id: number;
  nome: string;
  idade: number;
}

export interface CriarPessoa {
  nome: string;
  idade: number;
}

export interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: number;
  pessoa?: Pessoa;
}

export interface CriarTransacao {
  descricao: string;
  valor: number;
  tipo: TipoTransacao;
  pessoaId: number;
}

export interface PessoaTotal {
  id: number;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface TotaisResponse {
  pessoas: PessoaTotal[];
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}
