

using ShiftsLoggerUI;
class Program
{
    static async Task Main(string[] args)
    {
        UserInterface userInterface = new UserInterface();
        await userInterface.StartApp();
    }
}