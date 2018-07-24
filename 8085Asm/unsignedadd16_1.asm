; Add 2 16 bit unsigned intergers at 0200h, 0202h, save the result
; in 0204h
; Number format	M[0] = L, M[1]=H
; Written by Arnav Mukhopadhyay
;
; Dec 12, 2011
	
                LHLD 0200H
                MOV B, H
                MOV C, L
                LHLD 0202H
                DAD B
                SHLD 0204H
                HLT
