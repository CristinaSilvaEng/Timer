using System;
using System.Windows.Forms;
using Timer.Presenter;

namespace Timer.View
{
	public partial class MainWindow : Form
    {
		/// <summary>
		/// Presenter desta visão. Objeto é construido no construtor desta visão.
		/// </summary>
        private MainPresenter _presenter;

		/// <summary>
		/// Intervalo definido para o timer.
		/// </summary>
		private int _inteval;

		/// <summary>
		/// Inicializa componentes de tela e constroi presenter desta visão.
		/// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this._presenter = new MainPresenter();

			// O método Tick é chamado cada vez que o evento OnTick é chamado no presenter.
			this._presenter.OnTick += this.Tick;
        }

		/// <summary>
		/// Atualiza a label com as informações da próxima verificação.
		/// </summary>
		/// <param name="sender">Presenter.</param>
		/// <param name="e">Sem argumentos.</param>
		private void Tick(object sender, EventArgs e)
		{
			// Delegate necessario para mudar UI a partir de outra thread.
			this.Invoke((MethodInvoker)delegate
			{
				this.uxLblDtmNextTick.Text = DateTime.Now.AddMilliseconds(this._inteval)
					.ToString("dd/MM/yyyy HH:mm:ss");
			});
		}

		/// <summary>
		/// Evento lançado ao click do botão. Converte valores informados em milisegundos.
		/// </summary>
		/// <param name="sender">Botão uxBtnSetTimer.</param>
		/// <param name="e">Argumentos do evento.</param>
		private void uxBtnSetTimer_Click(object sender, EventArgs e)
        {
            int hours = Int32.Parse(uxNumUpDownHours.Text);
            int minutes = Int32.Parse(uxNumUpDownMinutes.Text);
            int seconds = Int32.Parse(uxNumUpDownSeconds.Text);

			// Calculo para converte horas, minutos e segundos em milisegundioss.
			this._inteval = ((hours * 60 * 60) + (minutes * 60) + seconds) * 1000;
			this._presenter.SetInterval(this._inteval);
        }
	}
}
