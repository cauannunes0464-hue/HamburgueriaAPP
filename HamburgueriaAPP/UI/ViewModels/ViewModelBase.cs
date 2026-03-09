using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HamburgueriaAPP.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged  // Classe base para ViewModels, implementa INotifyPropertyChanged para notificar a UI sobre mudanças nas propriedades
    {
        public event PropertyChangedEventHandler? PropertyChanged;     // Evento que é disparado quando uma propriedade é alterada

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) // Método protegido para disparar o evento PropertyChanged, usando CallerMemberName para obter o nome da propriedade automaticamente
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty <T> (ref T campo, T valor, [CallerMemberName] string? nome = null)
        {
            if (Equals(campo, valor))
                return false;

            campo = valor;

            OnPropertyChanged(nome);

            return true;
        }
    }

}