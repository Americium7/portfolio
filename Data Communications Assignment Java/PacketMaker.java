/*
A simple UDP Packet Maker that can make packets and add, check and remove checksum.  
Author: Antonio  Marotta
@1.2
*/  
import java.io.*;
import java.util.ArrayList;

class PacketMaker{
	public static final byte checkSum(byte[] bytes) {
   		byte sum = 0;
   		for (byte b : bytes) {
   		   sum ^= b;
   		}
   		return sum;
	}

	public static byte[] makePacketForSend(byte[] data){
		byte[] newPacket = new byte[data.length+1];
		for(int i=0; i<data.length; i++){
			newPacket[i] = data[i];
		}
		newPacket[newPacket.length-1] = checkSum(data);		
		return newPacket;
	}

	public static ArrayList<byte[]> makePackets(String fileName){
		ArrayList<byte[]> Packets = new ArrayList<byte[]>();
		try{
			BufferedInputStream reader = new BufferedInputStream(new FileInputStream(fileName));
			byte[] buff = new byte[100];
			int len;
			while( (len = reader.read(buff)) != -1){
				Packets.add(buff);
				buff = new byte[100];
			}
	
		}catch(Exception ex){
			System.out.println("File not found"); 
			return null;
		}
		return Packets;
	}

	public static boolean isChecksumCorrect(byte[] packet){
		byte carriedCheckSum = packet[packet.length-1];
		byte[] extract = new byte[packet.length-1];
		for(int i=0; i<extract.length; i++){
			extract[i] = packet[i];
		}

		//calculate checksum
		byte newCheckSum = checkSum(extract);

		if(newCheckSum == carriedCheckSum)
			return true;
		else
			return false;
	}
	
	public static byte[] removeChecksum(byte[] packet){
		byte[] newPacket = new byte[packet.length-1];
		for(int i=0; i<newPacket.length; i++)
			newPacket[i] = packet[i];
		return newPacket;
	}
}