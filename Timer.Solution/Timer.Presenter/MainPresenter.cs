using System;
using System.Timers;

namespace Timer.Presenter
{
	public class MainPresenter
    {
		/// <summary>
		/// Timer. Aguarda intervalo e chama evento.
		/// </summary>
		private System.Timers.Timer _timer;

		/// <summary>
		/// Define intervalo para o Timer.
		/// </summary>
		/// <param name="milliseconds">Millisegundos do intervalo.</param>
		public void SetInterval(int milliseconds)
		{
			// Caso exista algum timer em execução, o mesmo é Stopado.
			if (this._timer != null && this._timer.Enabled == true)
			{
				this._timer.Stop();
			}

			this._timer = new System.Timers.Timer();
			// Define qual o método deve ser chamado a cada Tick do timer.
			this._timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			// Define o intevalo em milisegundos entre cada Tick do timer.
			this._timer.Interval = milliseconds;
			this._timer.Enabled = true;
		}

		/// <summary>
		/// Evento lançado para informar a camada View que o timer chamou o método.
		/// </summary>
		public EventHandler OnTick;

		/// <summary>
		/// Método chamado pelo timer a cada intervalo definido na propriedade Interval.
		/// </summary>
		/// <param name="source">Timer.</param>
		/// <param name="e">Argumentos do evento.</param>
		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("Tick at {0}.", DateTime.Now.ToString("HH:mm:ss"));

			// Lança evento que é ouvido pelo View.
			OnTick(this, EventArgs.Empty);
		}
    }
}
