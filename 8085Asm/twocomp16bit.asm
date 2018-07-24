; find 2's complement of a 16 bit number X and store in next memory location
; to X
; author Arnav Mukhopadhyay
; Feb 27 2011

     ORG C000H
     LXI H, C100H
     MOV A, M
     CMA
     INR A
     MOV E, A
     MVI A, 00H
     ADC A
     MOV B, A
     INX H
     MOV A, M
     CMA
     ADD B
     MOV D, A
     INX H
     MOV M, E
     INX H
     MOV M, D
     HLT
  

