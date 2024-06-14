using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartBoard.Models;
using System.IO.Ports;

namespace Smartboard.Controllers;

public class ArduinoController : Controller
{
    private static SerialPort _serialPort;

    static ArduinoController()
    {
        try
        {
            _serialPort = new SerialPort("COM4", 9600); // lembrar de trocar
            if (!_serialPort.IsOpen)
            {
                _serialPort.Open();
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access to the port is denied: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IO error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult TurnOffLed()
    {
        string tosend = "08";
        SendString(tosend); // comando 0, porta 8
        return RedirectToAction("HomeClient", "DeviceClient");
    }

    [HttpPost]
    public IActionResult TurnOnLed()
    {
        SendString("18"); // comando 1, porta 8
        return RedirectToAction("HomeClient", "DeviceClient");
    }

    [HttpPost]
    public IActionResult SendText(string inputText)
    {
        string command = '2' + inputText;
        SendString(command);
        return RedirectToAction("HomeClient", "DeviceClient");
    }

    [HttpPost]
    public IActionResult SetAlarm(char alarmSeconds)
    {
        SendCommand(alarmSeconds);
        return RedirectToAction("HomeClient", "DeviceClient");
    }

    private void SendCommand(char command)
    {
        if (_serialPort.IsOpen)
        {
            _serialPort.Write(command.ToString());
        }
    }

    private void SendString(string command)
    {
        if (_serialPort.IsOpen)
        {
             _serialPort.Write(command.ToString());

        }
    }
}