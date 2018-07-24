; Assume a 8 bit number XYH in memory location 0200h. Fetch the
; number and store 0Xh and 0Yh in the 2 memory locations.
; Written By Arnav Mukhopadhyay
;
; Nov 08, 2011

	LDA 0200H
	ANI 0FH
	STA 0202H
	LDA 0200H
	RRC
	RRC
	RRC
	RRC
	ANI 0FH
	STA 0201H
	HLT
