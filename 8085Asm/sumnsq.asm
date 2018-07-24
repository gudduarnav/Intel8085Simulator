; find sum of n2 using sum of n odd numbers
; author Arnav Mukhopadhyay
; 15/04/2012

     lxi h, 0200h
     mov d, m
     xra a
     mov b, a
     inr b
loop:add b
     inr b
     inr b
     dcr d
     jnz loop
     inx h
     mov m, a
     hlt
  