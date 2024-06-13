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
    public IActionResult TurnOnLed()
    {
        SendCommand('1');
        return RedirectToAction("HomeClient", "DeviceClient");
    }

    [HttpPost]
    public IActionResult TurnOffLed()
    {
        SendCommand('0');
        return RedirectToAction("HomeClient", "DeviceClient");
    }

    [HttpPost]
    public IActionResult SendText(string inputText)
    {
        SendCommand('2');
        SendString(inputText);
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

        private void SendString(string text)
    {
        if (_serialPort.IsOpen)
        {
            _serialPort.Write(text);
        }
    }
}