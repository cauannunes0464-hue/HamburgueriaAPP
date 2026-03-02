using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using HamburgueriaAPP.Domain;

namespace HamburgueriaAPP.ViewModels
{
    
    public class ClienteViewModel : ViewModelBase
    {
        
        public ObservableCollection<Cliente> Clientes { get; set; } 

        private string _nome = string.Empty;
        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                OnPropertyChanged(nameof(Nome));

                AtualizarEstadoDoBotao();
            }
        }

        private string _telefone = string.Empty;
        public string Telefone
        {
            get => _telefone;
            set
            {
                _telefone = value;
                OnPropertyChanged(nameof(Telefone)); 

                AtualizarEstadoDoBotao();
            }
        }


        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));

                AtualizarEstadoDoBotao();
            }
        }


        public RelayCommand AdicionarClienteCommand { get; }
        private void AdicionarOuAtualizarCliente()
        {
            if (ClienteSelecionado == null)
            {
                var novoCliente = new Cliente(Nome, Telefone, Email);
                Clientes.Add(novoCliente);
            }

            else
            {
                ClienteSelecionado.AtualizarDados(Nome, Telefone, Email);
            }

            LimparCampos();

        }

        private bool PodeAdicionarCliente()
        {
            return
                !string.IsNullOrWhiteSpace(Nome) &&
                !string.IsNullOrWhiteSpace(Telefone) &&
                EmailValido(Email);
        }

        public RelayCommand RemoverClienteCommand { get; }
        private void RemoverCliente()
        {
            if (ClienteSelecionado != null)
            {
                Clientes.Remove(ClienteSelecionado);
            }
        }

        private bool PodeRemoverCliente()
        {
            return ClienteSelecionado != null;
        }


        public bool TemClienteSelecionado => ClienteSelecionado != null;

        private Cliente? _clienteSelecionado; 
        public Cliente? ClienteSelecionado 
        {
            get => _clienteSelecionado;
            set
            {
                _clienteSelecionado = value;
                OnPropertyChanged(nameof(ClienteSelecionado));
                OnPropertyChanged(nameof(TemClienteSelecionado));


                AtualizarEstadoDoBotao();

                if (value != null)
                {
                    Nome = value.Nome;
                    Telefone = value.Telefone;
                    Email = value.Email;
                }

            }
        }

        private bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern); // 
        }

        public ClienteViewModel()
        {
            Clientes = new ObservableCollection<Cliente>();

            AdicionarClienteCommand = new RelayCommand(
                AdicionarOuAtualizarCliente,
                PodeAdicionarCliente
                );

            RemoverClienteCommand = new RelayCommand(
                RemoverCliente,
                PodeRemoverCliente
                );
        }

        
        private void AtualizarEstadoDoBotao()
        {
            AdicionarClienteCommand.NotifyCanExecuteChanged();
            RemoverClienteCommand.NotifyCanExecuteChanged();
        }

        private void LimparCampos()
        {
            Nome = string.Empty;
            Telefone = string.Empty;
            Email = string.Empty;

            ClienteSelecionado = null;
        }
    }
}

