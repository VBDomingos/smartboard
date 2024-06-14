#include <Arduino.h>
#include <LCD_I2C.h>

LCD_I2C lcd(0x27, 16, 2); 

char buzzer = 4;

int led; // 9

void setup() {
  pinMode(buzzer, OUTPUT);
  pinMode(led, OUTPUT);
  Serial.begin(9600);

  lcd.begin();
  lcd.backlight();
}

void loop() {
  if (Serial.available()) {
    delay(50);
    char command = Serial.read();
    if (command == '0') {
      char ledChar = Serial.read();
      led = ledChar - '0';
      digitalWrite(led, LOW);
    }
    if (command == '1') {
      char ledChar = Serial.read();
      led = ledChar - '0';
      digitalWrite(led, HIGH);
    }
    if (command == '2') {
      String str = Serial.readString();
      lcd.print(str);
    }
    if (command == '3') {
      delay(1000);
      digitalWrite(buzzer, HIGH);
      delay(1000);
      digitalWrite(buzzer, LOW);
    }
    if (command == '4') {
      delay(2000);
      digitalWrite(buzzer, HIGH);
      delay(1000);
      digitalWrite(buzzer, LOW);
    }
    if (command == '5') {
      delay(3000);
      digitalWrite(buzzer, HIGH);
      delay(1000);
      digitalWrite(buzzer, LOW);
    }
  }
}