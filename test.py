import requests
import json
import random
import time

url = "https://gamejam.dnorhoj.dk/api/submissions"
randomText_main = ["gamejam is cool","i love gamejam","this is cool","we broot forced it lol","this is so cool","hello :)","wow 8-)","we won lol","so cool","this is fun"]
randomText_second = ["it realy is","yes i do","yes it is ;)","we realy did","wow it realy is","hi","smiley","yes i think so"," cool cool cool","i love it"]


with open('firstnames_m.json', 'r') as f:
    names = json.load(f) 


with open('firstnames_f.json', 'r') as f:
    names.append(json.load(f)) 


while True:

    rand_number = random.randrange(1,len(names))
    rand_number_sleeptime = random.randrange(1,10)
    randomText_main_end = names[rand_number]
    randomText_second_end = names[rand_number-1]

    name = random.randint(1,100)

    payload = json.dumps({
        "name": randomText_main_end,
        "content": randomText_second_end,
        "color": "#72139B"
    })
    headers = {
        'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64; rv:106.0) Gecko/20100101 Firefox/106.0',
        'Content-Type': 'application/json'
    }

    response = requests.request("POST", url, headers=headers, data=payload)

    print(response.text)
    print(payload)
    time.sleep(rand_number_sleeptime / 100)

