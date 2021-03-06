<Query Kind="Program">
  <Reference Relative="..\src\FluentJdf\bin\Debug\Infrastructure.Core.dll">C:\development\FluentJdf\src\FluentJdf\bin\Debug\Infrastructure.Core.dll</Reference>
  <Reference Relative="..\src\FluentJdf\bin\Debug\FluentJdf.dll">C:\development\FluentJdf\src\FluentJdf\bin\Debug\FluentJdf.dll</Reference>
  <Reference Relative="..\src\Tests\FluentJdf.Tests\bin\Debug\Castle.Core.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Castle.Core.dll</Reference>
  <Reference Relative="..\src\Tests\FluentJdf.Tests\bin\Debug\Castle.Windsor.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Castle.Windsor.dll</Reference>
  <Reference Relative="..\src\Tests\FluentJdf.Tests\bin\Debug\Infrastructure.Container.CastleWindsor.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Infrastructure.Container.CastleWindsor.dll</Reference>
  <Reference Relative="..\src\Tests\FluentJdf.Tests\bin\Debug\Infrastructure.Logging.NLog.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Logging.NLog\bin\Debug\Infrastructure.Logging.NLog.dll</Reference>
  <Reference Relative="..\src\Tests\FluentJdf.Tests\bin\Debug\NLog.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Logging.NLog\bin\Debug\NLog.dll</Reference>
  <Namespace>FluentJdf.LinqToJdf</Namespace>
  <Namespace>Infrastructure.Container.CastleWindsor</Namespace>
  <Namespace>Infrastructure.Logging.NLog</Namespace>
  <Namespace>FluentJdf.Configuration</Namespace>
  <Namespace>NLog.Config</Namespace>
  <Namespace>NLog.Targets</Namespace>
</Query>

bool loggingOn = false;

Ticket GetJdf() {
	return Ticket.CreateProcessGroup().AddIntent().WithInput().BindingIntent().ValidateJdf().Ticket;
}
	
Message GetJmf() {
	return Message.Create().AddCommand().SubmitQueueEntry().AddQuery().QueueStatus().ValidateJmf().Message;
}

void Main()
{
	InitializeFluentJdf();
	
	var ticket = GetJdf().Dump();
	ticket.ValidationMessages.Dump();
	"*****************".Dump();
	
	var message = GetJmf().Dump();
	message.ValidationMessages.Dump();
	"*****************".Dump();
}

void InitializeFluentJdf() {
	var config = Infrastructure.Core.Configuration.Settings.UseCastleWindsor();
	if (loggingOn){
		config.LogWithNLog(GetNLogConfiguration());
	}
	config.Configure();
	FluentJdfLibrary.Settings.ResetToDefaults();
}

LoggingConfiguration GetNLogConfiguration(){
	LoggingConfiguration config = new LoggingConfiguration();
	
	ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget();
	config.AddTarget("console", consoleTarget);
	consoleTarget.Layout = @"${longdate} ${level:uppercase=true} ${logger} ${newline}${message}${onexception:${newline}EXCEPTION OCCURRED\:${newline}${exception:format=tostring:maxInnerExceptionLevel=5}}${newline}";
	LoggingRule rule = new LoggingRule("*", NLog.LogLevel.Debug, consoleTarget);
	config.LoggingRules.Add(rule);
	
	return config;
}