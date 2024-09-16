using AutoMapper;
using Newtonsoft.Json;
using ProjetoEmprestimoLivros.Dto.Relatorio;
using ProjetoEmprestimoLivros.Enum;
using System.Data;

namespace ProjetoEmprestimoLivros.Services.RelatorioService
{
    public class RelatorioService : IRelatorioInterface
    {
        readonly IMapper _mapper;
        public RelatorioService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DataTable ColetarDados<T>(List<T> dados, int idRelatorio)
        {
            DataTable dataTable = new DataTable();

            if (dados == null || !dados.Any())
            {
                throw new InvalidOperationException("A lista de dados está vazia.");
            }

            //O TableName vai ser referente o id do relatório que recebemos
            dataTable.TableName = RelatorioEnum.GetName(typeof(RelatorioEnum), idRelatorio);
            //pegando o primeiro elemento da lista e pegando as propriedades dele, a propriedade é o nome da coluna dos Dtos cada propriedade é uma coluna
            var colunas = dados.First().GetType().GetProperties();

            // Adiciona as colunas ao DataTable, lidando com tipos nulos
            foreach (var coluna in colunas)
            {
                // Verifica se a coluna é Nullable, e se for, utiliza o tipo subjacente
                var tipoColuna = Nullable.GetUnderlyingType(coluna.PropertyType) ?? coluna.PropertyType;
                dataTable.Columns.Add(coluna.Name, tipoColuna); // Adiciona o nome e o tipo corrigido da coluna
            }

            switch (idRelatorio)
            {
                case 1:
                    var dadosLivro = JsonConvert.SerializeObject(dados); //Estamos recebemos em Json e transformando em string
                    var dadosLivroDto = JsonConvert.DeserializeObject<List<LivroRelatorioDto>>(dadosLivro); //Estamos transformando a string em um objeto do tipo LivroRelatorioDto
                    if (dadosLivroDto != null)
                    {
                        return ExportarLivro(dataTable, dadosLivroDto);
                    }
                    break;
                case 2:
                    var dadosClientes = JsonConvert.SerializeObject(dados);
                    var dadosClientesModel = JsonConvert.DeserializeObject<List<UsuarioRelatorioDto>>(dadosClientes);
                    if (dadosClientesModel != null)
                    {
                        return ExportarUsuario(dataTable, dadosClientesModel);
                    }
                    break;
                case 3:
                    var dadosFuncionarios = JsonConvert.SerializeObject(dados);
                    var dadosFuncionariosModel = JsonConvert.DeserializeObject<List<UsuarioRelatorioDto>>(dadosFuncionarios);
                    if (dadosFuncionariosModel != null)
                    {
                        return ExportarUsuario(dataTable, dadosFuncionariosModel);
                    }
                    break;
                case 4:
                    var dadosEmprestimosDevolvidos = JsonConvert.SerializeObject(dados); //Recebendo o Json e transformando em string
                    var dadosEmprestimosDevolvidosDto = JsonConvert.DeserializeObject<List<EmprestimoRelatorioDto>>(dadosEmprestimosDevolvidos); //Transformando a string em um objeto do tipo EmprestimoRelatorioDto
                    if (dadosEmprestimosDevolvidosDto != null) 
                    {
                        return ExportarEmprestimos(dataTable, dadosEmprestimosDevolvidosDto);
                    }
                    break;
                case 5: 
                    var dadosEmprestimosPendentes = JsonConvert.SerializeObject(dados); //Recebendo o Json e transformando em string
                    var dadosEmprestimosPendentesDto = JsonConvert.DeserializeObject<List<EmprestimoRelatorioDto>>(dadosEmprestimosPendentes); //Transformando a string em um objeto do tipo EmprestimoRelatorioDto
                    if( dadosEmprestimosPendentesDto != null)
                    {
                        return ExportarEmprestimos(dataTable, dadosEmprestimosPendentesDto);
                    }
                    break;
            }
            return new DataTable();
        }
        public DataTable ExportarLivro(DataTable data, List<LivroRelatorioDto> dados)
        {
            //pra cada registro que temos vamos adicionar uma linha na tabela do excel
            foreach (var dado in dados)
            {
                //a sequencia dos dados deve ser na msm ordem do nosso LivroRelatoDto
                data.Rows.Add(dado.Id, dado.Titulo, dado.Descricao, dado.ISBN, dado.Autor, dado.Genero, dado.AnoPublicacao, dado.QuantidadeEmEstoque);

            }//Depos de passar pelo foreach e criar cada uma das linhas vamos retornar a tabela
            return data;
        }
        public DataTable ExportarUsuario(DataTable data, List<UsuarioRelatorioDto> dados)
        {
            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.NomeCompleto, dado.Usuario, dado.Email, dado.Status =="True" ? "Ativo" : "Inativo", dado.Perfil, dado.Turno, dado.DataCadastro, dado.DataAlteracao, dado.Logradouro, dado.Bairro, dado.Numero, dado.CEP, dado.Estado, dado.Complemento);
            }
            return data;
        }
        public DataTable ExportarEmprestimos(DataTable data, List<EmprestimoRelatorioDto> dados)
        {
            foreach (var dado in dados)
            {
                data.Rows.Add(dado.Id, dado.UsuarioId, dado.NomeUsuario, dado.Usuario, dado.LivroId, dado.Titulo, dado.ISBN, dado.DataEmprestimo, dado.DataDevolucao);
            }
            return data;
        }
   
    }
}
