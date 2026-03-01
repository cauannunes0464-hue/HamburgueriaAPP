using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HamburgueriaAPP.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged  // Classe base para ViewModels, implementa INotifyPropertyChanged para notificar a UI sobre mudanças nas propriedades
    {
        public event PropertyChangedEventHandler? PropertyChanged;     // Evento que é disparado quando uma propriedade é alterada

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}