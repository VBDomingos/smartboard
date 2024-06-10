#include <Arduino.h>

void setup() {
  pinMode(10, OUTPUT);
  Serial.begin(9600);
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
  }
}