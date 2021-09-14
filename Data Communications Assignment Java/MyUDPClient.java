/*
A UDP Client with error handling that receives packets from the server, sends an ACK and decodes into a directory.
Author: Antonio  Marotta
@1.3
*/  
import java.net.*;
import java.io.*;

class MyUDPClient{
	public static void main(String str[]){
		try{
			System.out.println("Hi Antonio,");
			System.out.println("Welcome To Client");
      			BufferedReader inFromUser =
      			new BufferedReader(new InputStreamReader(System.in));
      			DatagramSocket clientSocket = new DatagramSocket();
      			InetAddress IPAddress = InetAddress.getByName("localhost");
      			byte[] sendRequest;
      			String fileRequested;
			
			sendRequest = new byte[1024];
        		System.out.println("Enter requested file name: ");
       			fileRequested = inFromUser.readLine();
       			sendRequest = fileRequested.getBytes();
        		DatagramPacket sendReqPacket = new DatagramPacket(sendRequest, sendRequest.length, IPAddress, 4000);
     			clientSocket.send(sendReqPacket);
			System.out.println("Receiving...");
				
			int counter = 0;
			new File("Received Files").mkdir();
			BufferedOutputStream writer = new BufferedOutputStream(new FileOutputStream("Received Files\\" + fileRequested));
      			while(true)    
			{
				byte[] receiveData = new byte[101];			
      			  	DatagramPacket receivePacket = new DatagramPacket(receiveData, receiveData.length);
				clientSocket.receive(receivePacket);

				//Sending ACK NACK
				//Gathering it again
				InetAddress IP = receivePacket.getAddress();
		        	int port2 = receivePacket.getPort();
               			String ackNack = "ACK";
				DatagramPacket response = new DatagramPacket(ackNack.getBytes(), ackNack.getBytes().length, IP, port2);
				clientSocket.send(response);

				String decode = new String(receiveData);
				
				if(decode.contains("Sorry")){
					System.out.println(decode.trim());
					break;
				}

      			  	//Received File
	
				if(!decode.contains("/q")){
					byte[] toWrite = PacketMaker.removeChecksum(receiveData);
					
					//System.out.println(++counter + ") Receiving packet contains 1st byte : " + toWrite[0]);
										
					writer.write(toWrite, 0, toWrite.length);
					writer.flush();
				}else{
					System.out.println("File Received, Saved Succesfully!");
					break;
				}
	
      			}

			writer.close();
      			clientSocket.close();
		}catch(Exception ex){	
			ex.printStackTrace();
		}
	}
}