﻿using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace TrabalhosDoJunior.ConsoleApp
{
    internal class Program
    {
        static string nomeEquipamento = "";
        static string fabricanteEquipamento = "";
        static int idEquipamentos = 0;
        static int numeroDeSerieEquipamento = 0;
        static double precoEquipamento = 0;
        static DateTime dataFabricacaoEquipamento = DateTime.UtcNow;

        static int idChamados = 0;
        static string títuloChamados = "";
        static string descriçãoChamados = "";
        static int EquipamentosIdChamados = 0;
        static DateTime dataDeAberturaChamados = DateTime.UtcNow;
        
        static void Main(string[] args)
        {
            ArrayList listaIdEquipamentos, listaPrecoEquipamentos, listaNomeEquipamentos, listaNumeroDeSerieEquipamentos, listaDataEquipamentos, listaFabricanteEquipamentos;
            CriaListasParaEquipamentos(out listaIdEquipamentos, out listaPrecoEquipamentos, out listaNomeEquipamentos, out listaNumeroDeSerieEquipamentos, out listaDataEquipamentos, out listaFabricanteEquipamentos);
            ArrayList listaIdChamados, listaTitulosChamados, listaDescricaoChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados;
            CriaListasParaChamados(out listaIdChamados, out listaTitulosChamados, out listaDescricaoChamados, out listaIdDoEquipamentoNosChamados, out listaDataAberturaChamados);
            string resposta = "";
            while (resposta.ToUpper() != "S")
            {
                resposta = MostraMenuInicial();
                if (resposta == "1")
                {
                    CRUD_Equipamentos(listaIdEquipamentos, listaPrecoEquipamentos, listaNomeEquipamentos, listaNumeroDeSerieEquipamentos, listaDataEquipamentos, listaFabricanteEquipamentos, resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "2")
                {
                    CRUD_Chamados(listaIdChamados, listaTitulosChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados, listaDescricaoChamados, listaNomeEquipamentos, listaIdEquipamentos, resposta);
                    resposta = "";
                    continue;
                }
            }
        }

         static void CriaListasParaChamados(out ArrayList listaIdChamados, out ArrayList listaTitulosChamados, out ArrayList listaDescricaoChamados, out ArrayList listaIdDoEquipamentoNosChamados, out ArrayList listaDataAberturaChamados)
        {
            listaIdChamados = new ArrayList();
            listaTitulosChamados = new ArrayList();
            listaDescricaoChamados = new ArrayList();
            listaIdDoEquipamentoNosChamados = new ArrayList();
            listaDataAberturaChamados = new ArrayList();
        }

         static void CriaListasParaEquipamentos(out ArrayList listaIdEquipamentos, out ArrayList listaPrecoEquipamentos, out ArrayList listaNomeEquipamentos, out ArrayList listaNumeroDeSerieEquipamentos, out ArrayList listaDataEquipamentos, out ArrayList listaFabricanteEquipamentos)
        {
            listaIdEquipamentos = new ArrayList();
            listaPrecoEquipamentos = new ArrayList();
            listaNomeEquipamentos = new ArrayList();
            listaNumeroDeSerieEquipamentos = new ArrayList();
            listaDataEquipamentos = new ArrayList();
            listaFabricanteEquipamentos = new ArrayList();
        }

        #region Equipamentos

         static string CRUD_Equipamentos(ArrayList listaId, ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                Console.Clear();
                resposta = MostraMenuEquipamentos();
                if (resposta == "1")
                {
                    Console.Clear();
                    AdicionaEquipamentos(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante, resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "2")
                {
                    Console.Clear();
                    MostraTodosOsEquipamentos(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante);
                    resposta = "";
                    continue;
                }
                if (resposta == "3")
                {
                    Console.Clear();
                    ModificaUmEquipamentoEscolhido(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante, resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "4")
                {
                    Console.Clear();
                    DeletaEquipamentos(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante, resposta);
                    resposta = "";
                    continue;
                }

            }

            return resposta;
        }

         static string MostraMenuInicial()
        {
            string resposta;
            Console.WriteLine("Oque deseja fazer: ");
            Console.WriteLine(" 1- Equipamentos\n 2- Chamados\n S para Sair");
            resposta = Console.ReadLine();
            return resposta;
        }

         static string MostraMenuEquipamentos()
        {
            string resposta;
            Console.WriteLine("Menu Equipamentos: ");
            Console.WriteLine(" 1- Adicionar Equipamentos\n 2- Visualizar Todos os Equipamentos\n 3- Editar Equipamentos\n 4- Excluir Equipamentos\n S para Sair");
            resposta = Console.ReadLine();
            return resposta;
        }

         static void DeletaEquipamentos(ArrayList listaId, ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaId.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsEquipamentos(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante);
                    Console.WriteLine("Id de quem deseja deletar;");
                    int idParaDeletar = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaId.Count; i++)
                    {
                        if (listaId[i].Equals(idParaDeletar))
                        {
                            RemoveDasListasEquipamentos(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante, i);

                        }
                        else
                        {
                            MensagemDeErro("Id Inválido");
                        }
                    }
                }
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

         static void RemoveDasListasEquipamentos(ArrayList listaId, ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante, int i)
        {
            listaId.RemoveAt(i);
            listaNome.RemoveAt(i);
            listaPreco.RemoveAt(i);
            listaData.RemoveAt(i);
            listanumeroDeSerie.RemoveAt(i);
            listafabricante.RemoveAt(i);
        }

         static void ModificaUmEquipamentoEscolhido(ArrayList listaId, ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaId.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsEquipamentos(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante);
                    Console.WriteLine("Id de quem deseja modificar;");
                    int idParaModificar = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaId.Count; i++)
                    {
                        if (listaId[i].Equals(idParaModificar))
                        {
                            PegadadosDoUsuarioEquipamentos();
                            if (nomeEquipamento.Length < 6)
                            {
                                MensagemDeErro("O Nome deve ter no mínimo 6 caracters");
                                continue;
                            }
                            ModificaAsListasEquipamentos(listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante, i);
                            MostraSucessoAoUsuarioEquipamentos("Modificado com Sucesso!");
                        }
                        else
                        {
                            MensagemDeErro("Id Inválido");
                            break;
                        }
                    }
                }
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

         static void ModificaAsListasEquipamentos(ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante, int i)
        {
            listaNome[i] = nomeEquipamento;
            listaPreco[i] = precoEquipamento;
            listaData[i] = dataFabricacaoEquipamento;
            listanumeroDeSerie[i] = numeroDeSerieEquipamento;
            listafabricante[i] = fabricanteEquipamento;
        }

         static void AdicionaEquipamentos(ArrayList listaId, ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                PegadadosDoUsuarioEquipamentos();
                if (nomeEquipamento.Length < 6)
                {
                    MensagemDeErro("O Nome deve ter no mínimo 6 caracters");
                    continue;
                }
                AdicionaNasListasEquipamentos(listaId, listaPreco, listaNome, listanumeroDeSerie, listaData, listafabricante);
                MostraSucessoAoUsuarioEquipamentos("Adicionado com Sucesso!");
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

         static void MensagemDeErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

         static void MostraTodosOsEquipamentos(ArrayList listaId, ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante)
        {
            if (listaId.Count == 0)
            {
                MensagemDeErro("Nao existem valores na lista");
            }
            else
            {
                Console.WriteLine(" Id  | Nome do Equipamento  | Preço do Equipamento: | Data da fabricação | Número de série | Fabricante do Equipamento");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < listaId.Count; i++)
                {
                    DateTime dataModificada = (DateTime)listaData[i];                 
                    Console.WriteLine(" {0,-3} | {1,-20} | {2,-21} |  {3,-17} |  {4,-14} | {5,-20}", listaId[i], listaNome[i], listaPreco[i], dataFabricacaoEquipamento.ToString("dd/MM/yyyy"), listanumeroDeSerie[i], listafabricante[i]);
                }
            }
            Console.ReadKey();
        }

         static void MostraSucessoAoUsuarioEquipamentos(string mensagem)
        {
            Console.WriteLine("____________________________________________________________________________");
            Console.WriteLine($" Id : {idEquipamentos}\n Nome do Equipamento: {nomeEquipamento}\n Preço do Equipamento: {precoEquipamento}\n Data da fabricação: {dataFabricacaoEquipamento.ToString("dd/MM/yyyy")}\n Número de série: {numeroDeSerieEquipamento}\n Fabricante do Equipamento: {fabricanteEquipamento}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);          
            Console.ResetColor();
            Console.WriteLine("____________________________________________________________________________");
        }

         static void AdicionaNasListasEquipamentos(ArrayList listaId, ArrayList listaPreco, ArrayList listaNome, ArrayList listanumeroDeSerie, ArrayList listaData, ArrayList listafabricante)
        {
            idEquipamentos++;
            listaId.Add(idEquipamentos);
            listaNome.Add(nomeEquipamento);
            listaPreco.Add(precoEquipamento);
            listaData.Add(dataFabricacaoEquipamento);
            listanumeroDeSerie.Add(numeroDeSerieEquipamento);
            listafabricante.Add(fabricanteEquipamento);
        }

         static void PegadadosDoUsuarioEquipamentos()
        {
            Console.Write("Nome do Equipamento: ");
            nomeEquipamento = Console.ReadLine();
            Console.Write("Preço: ");
            precoEquipamento = Convert.ToDouble(Console.ReadLine());
            Console.Write("Numero de Serie: ");
            numeroDeSerieEquipamento = Convert.ToInt32(Console.ReadLine());
            Console.Write("Data de fabricação: ");
            dataFabricacaoEquipamento = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Fabricante: ");
            fabricanteEquipamento = Console.ReadLine();
        }

        #endregion

        #region Chamados
         static void PegadadosDoUsuarioChamados()
        {
            Console.Write("Titulo do Chamado: ");
            títuloChamados = Console.ReadLine();
            Console.Write("Descricao: ");
            descriçãoChamados = Console.ReadLine();
            Console.Write("Id do Equipamento: ");
            EquipamentosIdChamados = Convert.ToInt32(Console.ReadLine());
            Console.Write("Data de Abertura: ");
            dataDeAberturaChamados = Convert.ToDateTime(Console.ReadLine());
        }

         static void AdicinaChamados(ArrayList listaIdChamados, ArrayList listaTitulosChamados , ArrayList listaIdDoEquipamentoNosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados, ArrayList listaIdEquipamentos, ArrayList listaNomeEquipamento, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                PegadadosDoUsuarioChamados();
                AdicionaNasListasChamados(listaIdChamados, listaTitulosChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados, listaDescricaoChamados);
                MostraSucessoAoUsuarioChamados("Adicionado com Sucesso!", listaNomeEquipamento, listaIdEquipamentos);
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }
         static void AdicionaNasListasChamados(ArrayList listaIdChamados, ArrayList listaTitulosChamados, ArrayList listaIdDoEquipamentoNosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados)
        {
            idChamados++;                               
            listaIdChamados.Add(idChamados);
            listaTitulosChamados.Add(títuloChamados);
            listaIdDoEquipamentoNosChamados.Add(EquipamentosIdChamados);
            listaDataAberturaChamados.Add(dataDeAberturaChamados);
            listaDescricaoChamados.Add(descriçãoChamados);
        }
         static void MostraSucessoAoUsuarioChamados(string mensagem, ArrayList listaIdEquipamentos, ArrayList listaNomeEquipamento)
        {
            string nomeEquipamentoDoChamado = "";
            for (int i = 0; i < listaIdEquipamentos.Count; i++)
            {
                if (listaIdEquipamentos[i].Equals(EquipamentosIdChamados))
                {
                    nomeEquipamentoDoChamado = (string)listaNomeEquipamento[i];
                }
            }
            Console.WriteLine("____________________________________________________________________________");
            Console.WriteLine($"Id : {idChamados}, Título: {títuloChamados}, Nome do Equipamento: {nomeEquipamentoDoChamado}, data da abertura: {dataDeAberturaChamados.ToString("dd/MM/yyyy")}, Descrição: {descriçãoChamados}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.WriteLine("____________________________________________________________________________");
        }
         static void MostraTodosOsChamados(ArrayList listaIdChamados, ArrayList listaTitulosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados,ArrayList listaNomeEquipamento, ArrayList listaIdEquipamentos, ArrayList listaIdDoEquipamentoNosChamados)
        {
            string nomeEquipamentoDoChamado = "";
            if (listaIdChamados.Count == 0)
            {
                MensagemDeErro("Nao existem valores na lista");
            }
            else
            {
                Console.WriteLine(" Id  | Titulo     | Equipamento           | Data de Abertura | Descrição             | Dias Abertos");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < listaIdChamados.Count; i++)
                {
                    for (int j = 0; j < listaIdEquipamentos.Count; j++)
                    {
                        if (listaIdEquipamentos[j].Equals(listaIdDoEquipamentoNosChamados[i]))
                        {
                            nomeEquipamentoDoChamado = (string)listaNomeEquipamento[j];
                        }
                    }
                    TimeSpan diasAbertos = new TimeSpan();
                    DateTime dataModificada = (DateTime)listaDataAberturaChamados[i];
                    diasAbertos = DateTime.UtcNow - dataModificada;
                    Console.WriteLine(" {0,-3} | {1,-10} | {2,-21} |  {3,-15} |  {4,-20} | {5,-20}", listaIdChamados[i] , listaTitulosChamados[i] ,nomeEquipamentoDoChamado, dataModificada.ToString("dd / MM / yyyy") , listaDescricaoChamados[i], diasAbertos.Days); 
                }

            }
            
            Console.ReadKey();
        }

         static void ModificaUmChamadoEscolhido(ArrayList listaIdChamados, ArrayList listaTitulosChamados, ArrayList listaIdDoEquipamentoNosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados, ArrayList listaNomeEquipamento, ArrayList listaIdEquipamentos, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaIdChamados.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsChamados(listaIdChamados, listaTitulosChamados, listaDataAberturaChamados, listaDescricaoChamados, listaNomeEquipamento, listaIdEquipamentos, listaIdDoEquipamentoNosChamados);
                    Console.WriteLine("Id de quem deseja modificar;");
                    int idParaModificar = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaIdChamados.Count; i++)
                    {
                        if (listaIdChamados[i].Equals(idParaModificar))
                        {
                            PegadadosDoUsuarioChamados();
                            ModificaAsListasChamados(listaTitulosChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados, listaDescricaoChamados, i);
                            MostraSucessoAoUsuarioChamados("Modificado com Sucesso!", listaIdEquipamentos, listaNomeEquipamento);
                        }
                        else
                        {
                            MensagemDeErro("Id Inválido");
                        }
                    }                  
                }
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }
         static void ModificaAsListasChamados(ArrayList listaTitulosChamados, ArrayList listaIdDoEquipamentoNosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados, int i)
        {
            listaTitulosChamados[i] = títuloChamados;
            listaIdDoEquipamentoNosChamados[i] = EquipamentosIdChamados;
            listaDataAberturaChamados[i] = dataDeAberturaChamados;
            listaDescricaoChamados[i] = descriçãoChamados;
        }
         static void DeletaChamados(ArrayList listaIdChamados, ArrayList listaTitulosChamados, ArrayList listaIdDoEquipamentoNosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados, ArrayList listaNomeEquipamento, ArrayList listaIdEquipamentos, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                if (listaIdChamados.Count == 0)
                {
                    MensagemDeErro("Nao existem valores na lista");
                }
                else
                {
                    MostraTodosOsChamados(listaIdChamados, listaTitulosChamados, listaDataAberturaChamados, listaDescricaoChamados, listaNomeEquipamento, listaIdEquipamentos, listaIdDoEquipamentoNosChamados);
                    Console.WriteLine("Id de quem deseja deletar;");
                    int idParaDeletar = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaIdChamados.Count; i++)
                    {
                        if (listaIdChamados[i].Equals(idParaDeletar))
                        {
                            RemoveDasListasChamados(listaIdChamados, listaTitulosChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados, listaDescricaoChamados, i);
                        }
                        else
                        {
                            MensagemDeErro("Id Inválido");
                        }
                    }
                    
                               
                }              
                Console.Write("Sair: ");
                resposta = Console.ReadLine();
            }
        }

         static void RemoveDasListasChamados(ArrayList listaIdChamados, ArrayList listaTitulosChamados, ArrayList listaIdDoEquipamentoNosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados, int i)
        {
            listaIdChamados.RemoveAt(i);
            listaTitulosChamados.RemoveAt(i);
            listaIdDoEquipamentoNosChamados.RemoveAt(i);
            listaDataAberturaChamados.RemoveAt(i);
            listaDescricaoChamados.RemoveAt(i);
        }
         static string MostraMenuChamados()
        {
            string resposta;
            Console.WriteLine("Menu Chamados: ");
            Console.WriteLine(" 1- Adicionar Chamados\n 2- Visualizar Todos os Chamados\n 3- Editar Chamados\n 4- Excluir Chamados\n S para Sair");
            resposta = Console.ReadLine();
            return resposta;
        }
         static string CRUD_Chamados(ArrayList listaIdChamados, ArrayList listaTitulosChamados, ArrayList listaIdDoEquipamentoNosChamados, ArrayList listaDataAberturaChamados, ArrayList listaDescricaoChamados, ArrayList listaNomeEquipamento, ArrayList listaIdEquipamentos, string resposta)
        {
            while (resposta.ToUpper() != "S")
            {
                Console.Clear();
                resposta = MostraMenuChamados();
                if (resposta == "1")
                {
                    Console.Clear();
                    AdicinaChamados(listaIdChamados, listaTitulosChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados, listaDescricaoChamados, listaNomeEquipamento, listaIdEquipamentos, resposta); 
                    resposta = "";
                    continue;
                }
                if (resposta == "2")
                {
                    Console.Clear();
                    MostraTodosOsChamados(listaIdChamados, listaTitulosChamados, listaDataAberturaChamados, listaDescricaoChamados, listaNomeEquipamento, listaIdEquipamentos, listaIdDoEquipamentoNosChamados);
                    resposta = "";
                    continue;
                }
                if (resposta == "3")
                {
                    Console.Clear();
                    ModificaUmChamadoEscolhido(listaIdChamados, listaTitulosChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados, listaDescricaoChamados, listaNomeEquipamento, listaIdEquipamentos, resposta);
                    resposta = "";
                    continue;
                }
                if (resposta == "4")
                {
                    Console.Clear();
                    DeletaChamados(listaIdChamados, listaTitulosChamados, listaIdDoEquipamentoNosChamados, listaDataAberturaChamados, listaDescricaoChamados, listaNomeEquipamento, listaIdEquipamentos, resposta);
                    resposta = "";
                    continue;
                }
                

            }

            return resposta;
        }


        #endregion
    }
}