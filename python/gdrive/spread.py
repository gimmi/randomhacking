import gspread
gs = gspread.login('XXX@gmail.com', 'XXXXX')
gs.open_by_key
wb = gs.open_by_key('0AkfdcnLZpiJKdG1YcTlNVFhpMnJwZXBKekNCOEYwSGc')
ws = wb.worksheet('Spese')
print(ws.acell('A1').value)
print(ws.acell('A2').value)
print(ws.acell('A3').value)
print(ws.acell('B3').value)
