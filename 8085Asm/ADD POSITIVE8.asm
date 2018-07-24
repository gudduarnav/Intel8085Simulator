; add all positive numbers from an input array at C100H. Store result in 
; consecutive memory location, while store FFH if result is 16 bit

     ORG C000H
     LXI H, C100H
     MOV B, M
     MVI C, 00H
LOOP: INX H
     XRA A
     ADD M
     JM SKIP
     ADD C
     MOV C, A
     JNC SKIP
     MVI C, FFH 
SKIP: DCR B
     JNZ LOOP
     INX H
     MOV M, C
     HLT

     