from Player import Player
from Packet import *
import socket
import threading
import sys

# -------- GLOBAL VARS --------
Players = []
OnlinePlayers = 0
# -----------------------------
def start_server():
    PORT = 10102
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    serverAddress = ("192.168.1.13", PORT)
    try:
        sock.bind(serverAddress)
    except socket.error as e:
     print(e)
     sys.exit()
    return sock

def main():

    sock = start_server()
    sock.listen()
    print("Server started!")
    
    # Accept connections
    while True:
        connection = sock.accept()[0]
        connection.settimeout(10)
        player = Player(connection=connection)

        Players.append(player)
        thread = threading.Thread(target=manage_player, args=(connection))
        thread.run()


def manage_player(player):
    # First packet sent by client is USERNAME, PASSWORD, MODE
    while True:
        try:
            packet = player.get_packet()
        except socket.timeout:
            return

        if not packet.is_valid():
            packet = Packet("TYPE=" + LOGIN_SIGNUP_ERROR_TYPE + "MSG=" + "Unknown error. Please try again;")
            player.send_packet(packet)
            continue
            
            username, password = (packet.content['USERNAME'], packet.content['PASSWORD'])
        
        if packet.content['MODE'] == SIGNUP_MODE:
            #TOOD Register on BD
            
        else:
            #Check Login on BD


    # Login correct!
    player.username = username

    #TODO Get user data from database, assign id to "player" and send to Client
    

    # Loop until player disconnects
    while True:
        packet = player.get_packet()
        
        if not packet:
            continue

        if packet.does_propagation:
            for _player in Players:
                _player.send_packet(packet)
        
        if packet.type == DISCONNECT_TYPE:
            break

    disconnect_player(player)   
    return

def get_player_login(player):
    packet = player.get_packet()
    return (packet.content['USERNAME'], packet.content['PASS'])


def disconnect_player(player):
    global OnlinePlayers
    Players.remove(player)
    OnlinePlayers -= 1
    player.connection.close()

main()
