using POC.WPF.FilaDebitoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POC.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(var svc = new FilaDebitoService.FilaDebitoClient())
            {
                var cmd = new DebitarPrestacaoCommand()
                {
                    SequencialOperacao = 1,
                    SequencialPrestacao = 2,
                    Valor = 3,
                    Url = "http://localhost:59865/Services/FilaDebito/FilaDebito.svc"
                };

                svc.Dispatch(cmd);
            }
        }
    }
}
