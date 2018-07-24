; program to load 2 values from C100 and C101 and loop between them
; author Arnav Mukhopadhyay
; Feb 27, 2012

     ORG C000H
     LXI H, C100H
     MOV A, M
     INX H
     CMP M
     CP SWAPXY
     DCX H
     CALL LOOP
     HLT
SWAPXY: DCX H
     MOV A, M
     INX H
     MOV B, M
     MOV M, A
     DCX H
     MOV M, B
     INX H
     RET
LOOP: MOV A, M
     INX H
     MOV B, M
LP: INX H
    MOV M, A
    INR A
    CMP B
    RP
    JMP LP

    
