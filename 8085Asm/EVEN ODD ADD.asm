; add even and odd numbers in input array and store the result as 16bit.
; author Arnav Mukhopadhyay
; Feb 29 2012

     ORG C000H
     LXI H, C100H
     MOV D, M
     MVI B, 00H
     MVI C, 00H     
LOOP: INX H
     MOV A, M
     ANI 01H 
     JNZ ODD        
     MOV A, M
     ADD B
     MOV B, A
     JMP NXT
ODD: MOV A, M
     ADD C
     MOV C, A
NXT: DCR D
     JNZ LOOP
     INX H
     MOV M, B
     INX H
     MOV M, C

     HLT
