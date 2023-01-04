while true
do
  echo 'test\nhey'
  wget --no-check-certificate --quiet \
  --method POST \
  --timeout=0 \
  --header 'Content-Type: application/json' \
  --body-data '{"name":"...","content":"Hehe","color":"#72139B"}' \
   'https://gamejam.dnorhoj.dk/api/submissions'
done

