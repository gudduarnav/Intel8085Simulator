     LXI H, E100H
     LXI D, E200H
     MOV A, M

LOOP: INX H
     INX D

     MOV B, M
     XCHG
     MOV C, M
     MOV M, B
     XCHG
     MOV M, C

     DCR A
     JNZ LOOP

     HLT

