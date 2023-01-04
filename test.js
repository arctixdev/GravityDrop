var myHeaders = new Headers();
myHeaders.append("Content-Type", "application/json");

while (10==10){
var raw = JSON.stringify({
  "name": "...",
  "content": "Hehe",
  "color": "#72139B"
});

var requestOptions = {
  method: 'POST',
  headers: myHeaders,
  body: raw,
  redirect: 'follow'
};

fetch("https://gamejam.dnorhoj.dk/api/submissions", requestOptions)
  .then(response => response.text())
  .then(result => console.log(result))
  .catch(error => console.log('error', error));
}
