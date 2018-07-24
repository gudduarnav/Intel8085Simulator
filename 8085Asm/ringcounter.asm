; Implement a ring counter and demostrate bit display.
; Use Bit Shift Display (Port 03h)
; author Arnav Mukhopadhyay
; Dec 31, 2011
     MVI A, 80H
MAIN: OUT 03H
     RRC
     STA 0202H
     CALL DELAY
     LDA 0202H
     JMP MAIN
     HLT
DELAY: MVI A, FFH
DELAY1: NOP
     NOP
     NOP
     DCR A
     JNZ DELAY1
     RET
