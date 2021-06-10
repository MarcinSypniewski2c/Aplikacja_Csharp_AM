using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp_switching.ViewModel;
using WpfApp_switching.View;
using System.Windows.Media;
using System.Windows.Controls;

namespace WpfApp_switching.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;


        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Graph")
            {
                viewModel.SelectedViewModel = new GraphViewModel();
            }
            else if (parameter.ToString() == "Led")
            {
                viewModel.SelectedViewModel = new LedViewModel();
            }
            else if (parameter.ToString() == "List")
            {
                viewModel.SelectedViewModel = new ListViewModel();
            }
        }
    }
}
