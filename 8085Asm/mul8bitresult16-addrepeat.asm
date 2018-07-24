  
     LHLD E100H
     LXI D, 0000H
     MOV A, H 
     MVI H, 00H
     XCHG
LOOP: ADI 00H
     JZ STOP
     DAD D
     DCR A
     JMP LOOP
STOP:     SHLD E102H
     HLT



          