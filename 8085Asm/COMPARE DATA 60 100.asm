     LXI H, E100H
     LXI D, E200H
     MVI C, 0AH

LOOP: MOV A, M
     
     CPI 60
     JC NEXT
     JZ NEXT

     CPI 100
     JNC NEXT
     JZ NEXT

     XCHG
     MOV M, A
     XCHG
     INX D

NEXT: INX H

     DCR C
     JNZ LOOP

     HLT
