// See https://aka.ms/new-console-template for more information
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS0219 // Variable is assigned but its value is never used

long l1 = 0x_123_4567_89ab_cedf;
long l2 = 0x_123456789abcedf;
long l3 = 0x_12345_6789_abc_ed_f;

uint binary1 = 0b_1111_1110_1101_1100_1011_1010_1001_1000;
uint hex1 = 0x_fedcba98;
uint binary2 = 0b_111_110_101_100_011_010_001_000;
ushort binary3 = 0b1111_0000_101010_11;

decimal d = 12.30M;

#pragma warning restore CS0219 // Variable is assigned but its value is never used
#pragma warning restore IDE0059 // Unnecessary assignment of a value
