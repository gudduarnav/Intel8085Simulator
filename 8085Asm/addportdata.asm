; Add 2 numbers from decimal display (port 02h), add them and show the result
; on decimal display
; author Arnav Mukhopadhyay
; Dec 31, 2011

     IN 02H
     MOV B, A
     IN 02H
     ADD B
     OUT 02H
     HLT
     