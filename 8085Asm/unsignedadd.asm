; Add 2 unsigned numbers in locations 0200h, 0201h, 0202h
; Written by Arnav Mukhopadhyay
;
; 3 Nov, 2011
	
	LDA 0201H
	MOV B, A
	LDA 0200H
	ADD B
	STA 0202H
	HLT
