     LHLD E100H
     LXI D, 0000H

LOOP: XRA A
     ADD H
     JZ STOP

     CMP L
     JM STOP

     SUB L
     INR D
     MOV H, A
     JMP LOOP

STOP: MOV E, A
     XCHG
     SHLD E102H

     HLT


     

