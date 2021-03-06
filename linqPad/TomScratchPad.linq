<Query Kind="Program">
  <Reference Relative="..\src\FluentJdf\bin\Debug\Infrastructure.Core.dll">C:\development\FluentJdf\src\FluentJdf\bin\Debug\Infrastructure.Core.dll</Reference>
  <Reference Relative="..\src\FluentJdf\bin\Debug\FluentJdf.dll">C:\development\FluentJdf\src\FluentJdf\bin\Debug\FluentJdf.dll</Reference>
  <Reference Relative="..\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Castle.Core.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Castle.Core.dll</Reference>
  <Reference Relative="..\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Castle.Windsor.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Castle.Windsor.dll</Reference>
  <Reference Relative="..\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Infrastructure.Container.CastleWindsor.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Container.CastleWindsor\bin\Debug\Infrastructure.Container.CastleWindsor.dll</Reference>
  <Reference Relative="..\src\Infrastructure\Infrastructure.Logging.NLog\bin\Debug\Infrastructure.Logging.NLog.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Logging.NLog\bin\Debug\Infrastructure.Logging.NLog.dll</Reference>
  <Reference Relative="..\src\Infrastructure\Infrastructure.Logging.NLog\bin\Debug\NLog.dll">C:\development\FluentJdf\src\Infrastructure\Infrastructure.Logging.NLog\bin\Debug\NLog.dll</Reference>
  <Namespace>FluentJdf.LinqToJdf</Namespace>
  <Namespace>Infrastructure.Container.CastleWindsor</Namespace>
  <Namespace>Infrastructure.Logging.NLog</Namespace>
  <Namespace>FluentJdf.Configuration</Namespace>
  <Namespace>NLog.Config</Namespace>
  <Namespace>NLog.Targets</Namespace>
</Query>

bool loggingOn = true;

Ticket GetJdf() {
	return Ticket.CreateProcessGroup().AddIntent().WithInput().BindingIntent().ValidateJdf().Ticket;
}
	
Message GetJmf() {
	return Message.Create().AddCommand().SubmitQueueEntry().AddQuery().QueueStatus().ValidateJmf().Message;
}

void Main()
{
	InitializeFluentJdf();

	var xDoc = XDocument.Load(@"C:\development\fluentjdf\src\Tests\FluentJdf.Tests\TestData\ArtDeliveryIntentTest.jdf");
	var nm = new XmlNamespaceManager(new NameTable());
	nm.AddNamespace("jdf", "http://www.CIP4.org/JDFSchema_1_1");
	nm.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

	var intent = xDoc.XPathSelectElement(@".//jdf:ArtDeliveryIntent", nm);//.Dump();
	
	//now lets see what we can find.
	
	xDoc.SelectJDFDescendants(Resource.ArtDeliveryIntent).Dump().SelectJDFDescendant(Resource.RunList).Dump();
	
	xDoc.SelectJDFDescendant(Resource.ArtDeliveryIntent).Attribute("ID").Value.Dump();
	
	/*var ticket = GetJdf().Dump();
	ticket.ValidationMessages.Dump();
	"*****************".Dump();
	
	var message = GetJmf().Dump();
	message.ValidationMessages.Dump();
	"*****************".Dump();
	
	message.Transmit("http://localhost/jdf").Dump();*/
}

void InitializeFluentJdf() {
	var config = Infrastructure.Core.Configuration.Settings.UseCastleWindsor();
	if (loggingOn){
		config.LogWithNLog(GetNLogConfiguration());
	}
	config.Configure();
	FluentJdfLibrary.Settings.ResetToDefaults();
	Infrastructure.Core.Configuration.Settings.ServiceLocator.LogRegisteredComponents();
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