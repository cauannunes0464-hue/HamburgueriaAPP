using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using HamburgueriaAPP.ViewModels;
using Avalonia.Input;
using System.Linq;

namespace HamburgueriaAPP.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Definimos o DataContext
            DataContext = new ClienteViewModel();

            // Buscamos o Grid de filtros. O '!' diz que sabemos que ele existe no XAML.
            var filtroGrid = this.Find<UniformGrid>("FiltroGrid");


            
            if (filtroGrid != null)
            {
                // Percorremos apenas os RadioButtons dentro do Grid
                foreach (var radioButton in filtroGrid.Children.OfType<RadioButton>()) 
                {
                    radioButton.Checked += AplicarFiltro;
                }
            }
        }

        private void AplicarFiltro(object sender, RoutedEventArgs e)
        {
            // O 'as' tenta converter o sender para RadioButton com segurança
            if (sender is RadioButton rb)
            {
                // Usamos ?. e ?? para garantir que 'categoria' nunca seja nula
                string categoriaSelecionada = rb.Content?.ToString() ?? "TODOS";

                // Buscamos o painel de produtos usando o '!' para evitar o aviso de nulo
                var wrapPanelProdutos = this.Find<WrapPanel>("PainelProdutos");

                if (wrapPanelProdutos == null) return;

                foreach (var control in wrapPanelProdutos.Children)
                {
                    if (control is Button btn)
                    {
                        // Se for "TODOS", mostra o botão.
                        // Se não, compara a Tag do botão com a categoria selecionada.
                        if (categoriaSelecionada == "TODOS")
                        {
                            btn.IsVisible = true;
                        }
                        else
                        {
                            // Comparamos a Tag (que você colocou no XAML) com o filtro
                            string? btnTag = btn.Tag?.ToString();
                            btn.IsVisible = (btnTag == categoriaSelecionada);
                        }
                    }
                }
            }
        }

        // MÉTODOS DOS BOTÕES DA BARRA DE TÍTULO
        private void Fechar_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimizar_Click(object? sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximizar_Click(object? sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }


        private void Window_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            // Verifica se o botão esquerdo do mouse foi pressionado
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                this.BeginMoveDrag(e);
            }
        }


    }
}