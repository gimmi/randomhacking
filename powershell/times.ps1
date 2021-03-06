# Get-EventLog -LogName System | Where-Object { ($_.eventID -eq 6005) -or ($_.EventID -eq 6006) } | Format-Table -Property TimeGenerated,Message
# Get-WinEvent -LogName System -MaxEvents 10 | Where-Object { ($_.Id -eq 6005) -or ($_.Id -eq 6006) } | Format-Table -Property TimeCreated,Message | write-host

$days = @{}

Get-WinEvent -LogName System | Where-Object { $_.Id -eq 6005 } | ForEach-Object {
    $day = $_.TimeCreated.Date
    $startup = $_.TimeCreated
    if(-not $days.ContainsKey($day)) {
        $days.Add($day, @{ 'startup' = $startup })
    } elseif($days.Get_Item($day).Get_Item('startup') -gt $startup) {
        $days.Get_Item($day).Set_Item('startup', $startup)
    }
}

Get-WinEvent -LogName System | Where-Object { $_.Id -eq 6006 } | ForEach-Object {
    $day = $_.TimeCreated.Date
    $shutdown = $_.TimeCreated
    if(-not $days.ContainsKey($day)) {
        $days.Add($day, @{ 'shutdown' = $shutdown })
    } elseif($days.Get_Item($day).Get_Item('shutdown') -lt $shutdown) {
        $days.Get_Item($day).Set_Item('shutdown', $shutdown)
    }
}

$days.GetEnumerator() | ForEach-Object {
    Write-Host $_.Key.ToString('d')
}