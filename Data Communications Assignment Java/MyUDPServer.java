/*
A UDP Server with error handling that can send files to a client via small packets. 
Author: Antonio  Marotta
@1.3
*/  
import java.net.*;
import java.io.*;
import java.util.ArrayList;

class MyUDPServer{
	public static void main(String str[]){
		try{
        		DatagramSocket serverSocket = new DatagramSocket(null);
			serverSocket.setReuseAddress(true);
			serverSocket.bind(new InetSocketAddress(5000));
			System.out.println("Hi Antonio,");
			System.out.println("Server Started at Port 5000");
			byte[] receiveData = new byte[1024];
			
			int counter = 0;
			while(true){	
				//Listening to receive Packet
					receiveData = new byte[1024];
					//System.out.println("Waiting for client's call");
					DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);
	              			serverSocket.receive(receivePacket);
	            			String sentence = new String( receivePacket.getData()).trim();
	
					if(!sentence.startsWith("AC") && !sentence.startsWith("NAC")){
	
				//Gathering client information
					InetAddress IPAddress = receivePacket.getAddress();
			              	int port = receivePacket.getPort();
               
				//Packet Received
					System.out.println("Your requsted file : " + sentence);
					
					if(new File(sentence).exists()){
						System.out.println("Sending...");
						ArrayList<byte[]> packets = PacketMaker.makePackets(sentence);
						for(int i=0; i<packets.size(); i++){	
							byte[] sendablePack = PacketMaker.makePacketForSend(packets.get(i));
							
							DatagramPacket toBeSend = new DatagramPacket(sendablePack, sendablePack.length, IPAddress, port);
							//System.out.println(++counter + ") Sending packet contains 1st byte : " + packets.get(i)[0]);
							serverSocket.send(toBeSend);
							
							//ACK NACK Handling
							byte[] res = new byte[3];
							DatagramPacket response = new DatagramPacket(res, res.length);
							serverSocket.setSoTimeout(2000);
							try{
								serverSocket.receive(response);
							}catch(SocketTimeoutException e){i--;}
	
						}
	
						String quitMsg = "/quit";
						byte[] quitMsgBytes = quitMsg.getBytes();
						DatagramPacket quitIndicator = new DatagramPacket(quitMsgBytes, quitMsgBytes.length, IPAddress, port);
						serverSocket.send(quitIndicator);
						System.out.println("File Sent!");
					}else{
						String msg = "Sorry! Server does not contain this file";
						byte[] msgInBytes = msg.getBytes();
						DatagramPacket noFile = new DatagramPacket(msgInBytes, msgInBytes.length, IPAddress, port);
						serverSocket.send(noFile);
					}
				}
			}
		}catch(Exception ex){	
			//ex.printStackTrace();
		}
	}
}