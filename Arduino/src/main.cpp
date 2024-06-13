#include <Arduino.h>
#include <LCD_I2C.h>

LCD_I2C lcd(0x27, 16, 2); 

void setup() {
  pinMode(4, OUTPUT);
  pinMode(10, OUTPUT);
  Serial.begin(9600);

  lcd.begin();
  lcd.backlight();
}

void loop() {
  if (Serial.available()) {
    delay(50);
    char command = Serial.read();
    if (command == '1') {
      digitalWrite(10, HIGH);
    }
    if (command == '0') {
      digitalWrite(10, LOW);
    }
    if (command == '2') {
      String str = Serial.readString();
      lcd.print(str);
    }
    if (command == '3') {
      digitalWrite(4, HIGH);
      delay(1500);
      digitalWrite(4, LOW);
    }
  }
}
