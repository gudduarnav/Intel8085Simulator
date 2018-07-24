; show the effects of flag register
; author arnav Mukhopadhyay
; Feb 28, 2012

     ORG C000H
     LXI SP, F000H
     LXI H, C100H

     XRA A
     PUSH PSW
     POP B
     PUSH B
     MOV M, C
     INX H

     POP PSW
     MVI A, 00H
     PUSH PSW
     POP B
     PUSH B
     MOV M, C
     INX H

     POP PSW
     ORA A
     PUSH PSW
     POP B
     MOV M, C

     HLT

     