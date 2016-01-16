# Timer
Timer em C# (.NET Framework 4.0.3)

## Objetivo
O objetivo deste projeto é implementar um timer cujo intervalo seja informado por uma interface gráfica, a cada intervalo um método deveria ser chamado e a interface gráfica atualizada.

## Especificações
Esta é uma *Windows Forms Application*, porém não deve ser dificil aplicar os mesmos conceitos em uma *WPF Application*. Este projeto segue o padrão de arquitetura **MVP** (*Model-View-Presenter*), no obstante a camada Model neste caso tenha sido dispensada. Para a camada Presenter foi utilizado um *Class Library*. O *.NET Framework* utilizado é o **4.0.3**, por requisito técnico de onde este código será propriamente implementado.

A arquitetura MVP neste caso quer dizer que o projeto Timer.View depende do Timer. A camada Presenter envia eventos que são recebidos pelo View, enquanto que a View tem um objeto do Presenter e os métodos e propriedades deste são usados de forma transparente.

## Implementação

Detendo um objeto do tipo `MainPresenter` o `MainWindow` chama o método `uxBtnSetTimer_Click` quando o botão `uxBtnSetTimer` é clicado, lá os dados informados na *GUI* são convertidos para millisegundos e passado para o método `SetInterval` do `MainPresenter`.

Caso exista algum timer em operação, o mesmo é parado:
```cs
if (this._timer != null && this._timer.Enabled == true)
{
	this._timer.Stop();
}
```

Em seguida os valores de intervalo e que método deve ser chamado a cada intervalo são definidos:
```cs
this._timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
this._timer.Interval = milliseconds;
```

E o timer é ativo:
```cd
this._timer.Enabled = true;
```

O temporarizador utilziado é o [System.Timers.Timer](https://msdn.microsoft.com/pt-br/library/system.timers.timer(v=vs.110).aspx) disponivel no `.NET Framework` é mais adequado para *background tasks* do que o [System.Threading.Timer](https://msdn.microsoft.com/pt-br/library/system.threading.timer(v=vs.110).aspx), um artigo a esse respeito pode ser encontrado em [Comparing the Timer Classes in the .NET Framework Class Library](https://web.archive.org/web/20150329101415/https://msdn.microsoft.com/en-us/magazine/cc164015.aspx) citado [nesta questão no Stackoverflow](http://stackoverflow.com/questions/1416803/system-timers-timer-vs-system-threading-timer).

O método passado ao timer deve ter uma assinatura especifica, podendo apenas variar o nome do mesmo.

```cs
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		(...)
	}
```

A cada chamada do `OnTimedEvent` o evento `OnTick` é chamado. O mesmo é recebido pelo `MainWindow` que assinou o evento em seu construtor `this._presenter.OnTick += this.Tick;`. Cada vez que o evento é recebido o método `Tick` do `MainWindow` é chamado, e este é o responsavel por atualizar a interface grafica.

**Atenção**: Como este método foi chamado em outra *thread*, a *thread* do timer, se apenas especificarmos a mudança de um elemento na *GUI*, nada ocorrerá. Ou seja, o código abaixo não surte o efeito desejado:

```cs
this.uxLblDtmNextTick.Text = DateTime.Now.AddMilliseconds(this._inteval)
	.ToString("dd/MM/yyyy HH:mm:ss");
```

Por tanto deve-se modificar o código, para que o mesmo chame a *thread* dona do *Control* que se deseja modificar. O código resultante é o seguinte:

```cs
this.Invoke((MethodInvoker)delegate
{
	this.uxLblDtmNextTick.Text = DateTime.Now.AddMilliseconds(this._inteval)
		.ToString("dd/MM/yyyy HH:mm:ss");
});
```

:octocat:
