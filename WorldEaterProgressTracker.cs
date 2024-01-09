//MCCScript 1.0

//using System.Diagnostics;
//using System.Threading;
//using System.Threading.Tasks;

MCC.LoadBot(new ServicesChatBot());

//MCCScript Extensions

// The code and comments above are defining a "Script Metadata" section

// Every single chat bot (script) must be a class which extends the ChatBot class.
// Your class must be instantiates in the "Script Metadata" section and passed to MCC.LoadBot function.
class ServicesChatBot : ChatBot
{
	//condition constants
	const bool debugMode = false; //Display additional informations for debug
	const bool autoReconnect = true; //Whether try to reconnect when the connection is lost
	const string botName = "STT"; //Name of bot you want to track
	const int worldEaterStartPos = 478; //The minimun postion of the bot in the working axis 
	const int worldEaterEndPos = -1983; //The maximum coordinate of the bot in the working axis
	enum worldEaterDirections
	{
		xDirection,
		zDirection
	};
	const worldEaterDirections worldEaterDirection = worldEaterDirections.xDirection; //Choose a working direction of world eater between x-axis and y-axis
	const int runningRetrieveInterval = 30, stoppedRetrieveInterval = 60; //data retrieve intervals in seconds
	const int refreshInterval = 1000; //display refresh interval in milliseconds

	//Global variables
	int x = 0, z = 0, pos = 0, initialPos, differenceInPos = 0;
	float percentage = 0, timeInterval = 0, speed = 0, tps = 0;
	bool reversed = false, stopped = false;
	string message = null;
	enum ConnectionStates
	{
		justConnected,
		connected,
		disconnected,
		reconnecting
	};
	ConnectionStates connectionState = ConnectionStates.disconnected;
	System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
	Task loopTask = null;

	public void InitialProcess() //process all the data from Getmessage for the first time
	{
		//Get coordinates from message
		message = message.Remove(0, message.IndexOf('[') + 1);
		x = Int32.Parse(message.Substring(0, message.IndexOf(',')));
		message = message.Remove(0, message.IndexOf(' ') + 1);
		message = message.Remove(0, message.IndexOf(' ') + 1);
		z = Int32.Parse(message.Substring(0, message.IndexOf(']')));

		if (worldEaterDirection == worldEaterDirections.xDirection)
		{
			pos = x;
		}
		else if (worldEaterDirection == worldEaterDirections.zDirection)
		{
			pos = z;
		}

		//Calculate speed
		if (timeInterval != 0)
		{
			differenceInPos = pos - initialPos;
			speed = (float)differenceInPos / timeInterval;
		}

		//Process direction and stopped condition
		if (differenceInPos < 0)
		{
			reversed = false;
			stopped = false;
		}
		else if (differenceInPos > 0)
		{
			reversed = true;
			stopped = false;
        }
        else
        {
			stopped = true;
		}
		return;
	}

	public void LoopProcessAndOutput() //process and output every time in the loop
    {
		//Calculate the ratio of completion
		percentage = (float)(worldEaterStartPos - pos) / (float)(worldEaterStartPos - worldEaterEndPos) * (float)100;
		if (reversed)
        {
			percentage = (float)100 - percentage;
        }

		//Output
		if (debugMode == false) Console.Clear();
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("Position: " + pos);
		if (stopped == false)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Running: " + Math.Round(percentage, 2) + "%");
			string percentageBar = "[";
			for (int index = 0; index < percentage / 2; index++)
			{
				percentageBar = percentageBar + '■';
			}
			for (int index = 0; index < 50 - percentage / 2; index++)
			{
				percentageBar = percentageBar + ' ';
			}
			percentageBar = percentageBar + ']';
			Console.WriteLine(percentageBar);
			Console.ForegroundColor = ConsoleColor.White;
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Green;
            if (reversed == true)
            {
				Console.WriteLine("Stopped at start");
            }
            else
            {
				Console.WriteLine("Stopped at end");
			}
			Console.WriteLine("[■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■]");
			Console.ForegroundColor = ConsoleColor.White;
		}
		Console.WriteLine("TPS: " + Math.Round(tps, 2));
		if (debugMode == false)
        {
			Console.WriteLine("Speed: " + Math.Round(Math.Abs(speed), 2) + " m/s");
		}
		else
		{
			Console.WriteLine("Speed: " + Math.Round(speed, 2) + " m/s");
			Console.WriteLine("Difference in position: " + differenceInPos);
			Console.WriteLine("Time interval (s): " + timeInterval);
			Console.WriteLine("Reversed: " + reversed);
		}
		return;
	}

	public void LoopTask() //Timing thread
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("Loop task Created");
		Console.ForegroundColor = ConsoleColor.White;
		while (connectionState == ConnectionStates.connected || connectionState == ConnectionStates.justConnected || autoReconnect == true){
			if (connectionState == ConnectionStates.connected || connectionState == ConnectionStates.justConnected){
				message = null;
				SendText("!!vris " + botName);
				for (int index = 0; message == null && index <= 10; index ++)
				{
					Thread.Sleep(200);
					if (debugMode == true) Console.WriteLine("Waiting for message, message = " + message);
				}
				if (debugMode == true) Console.WriteLine("InitalProcess started");
				InitialProcess();
				initialPos = pos;

				//Sleep
				int currentRetrieveInterval;
				if (stopped == false){
					currentRetrieveInterval = runningRetrieveInterval;
				}else{
					currentRetrieveInterval = stoppedRetrieveInterval;
				}
				if (connectionState == ConnectionStates.justConnected)
                {
					currentRetrieveInterval = 5;
					connectionState = ConnectionStates.connected;
				}

				for (float index = 1; index <= (float)currentRetrieveInterval; index += refreshInterval / (float)1000)
                {
					if (connectionState != ConnectionStates.connected && connectionState != ConnectionStates.justConnected)
                    {
						break;
                    }
					if (debugMode == true) Console.WriteLine("LoopProcessAndOutput started");
					LoopProcessAndOutput();
					Thread.Sleep(refreshInterval);
					pos = (int)((float)initialPos + index * speed);
                }
			}else{
				Thread.Sleep(refreshInterval);
			}
		}
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("Loop task stopped");
		Console.ForegroundColor = ConsoleColor.White;
		return;
	}

    // This method will be called when the script has been initialized for the first time, it's called only once
	public override void Initialize()
	{
		connectionState = ConnectionStates.justConnected;
		Console.ForegroundColor = ConsoleColor.White;
		Stopwatch stopwatch = Stopwatch.StartNew();
		loopTask = Task.Run(LoopTask); //Run the timing thread
	}
	public override void AfterGameJoined()
	{
		if (stopwatch.IsRunning) stopwatch.Restart();
		connectionState = ConnectionStates.justConnected;
	}

	// This is a function that will be run when we get a chat message from a server
	public override void GetText(string text)
	{
		if (debugMode == true) Console.WriteLine("Message recieved");
		text = GetVerbatim(text);
		//LogToConsole("Recieved: " + text);
		if (!text.Contains(botName + " @ ")){
			if (debugMode == true) LogToConsole("Message Ignored");
			return;
        }
        else
        {
			message = text;

			//Interval timing
			stopwatch.Stop();
			timeInterval = (float)stopwatch.ElapsedMilliseconds / (float)1000;
			stopwatch.Restart();
		}
	}
	
	//Record TPS
	public override void OnServerTpsUpdate(double updatedTps) 
    { 
		tps = (float)updatedTps;
		if (debugMode == true) Console.WriteLine("Got TPS from OnServerTpsUpdate: " + tps);
    }

	//Disconnection Handlling
	public override bool OnDisconnect(DisconnectReason reason, string message)
	{
		if (connectionState == ConnectionStates.connected){
			if (autoReconnect == true){
				connectionState = ConnectionStates.reconnecting;
				ReconnectToTheServer(3,3);
				Console.WriteLine("Reconnecting");
			}else{
				connectionState = ConnectionStates.disconnected;
				Console.WriteLine("Disconnected");
			}
		}
		return false;
	}
}
