; Implement a delay algorithm when processor runs at 2 Mhz.
; author Arnav Mukhopadhyay
; Dec 31, 2011

MAIN:     MVI A, 00H
          OUT 03H
          CALL DELAY
          MVI A, 01H
          OUT 03H
          CALL DELAY
          JMP MAIN
          HLT
DELAY:    MVI A, FFH
DELAY1:   NOP 
          NOP
          NOP
          DCR A
          JNZ DELAY1
          RET
