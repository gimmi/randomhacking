use std::error::Error;
use std::net::UdpSocket;
use std::thread;
use std::time::{SystemTime, Duration};
use std::str::from_utf8;
use std::sync::mpsc;
use std::sync::mpsc::Receiver;
use std::io::ErrorKind;

#[derive(Debug)]
pub struct GelfMessage {
    host: String,
    container_name: String,
    short_message: String,
    epoch_ms: u64,
}

pub fn parse_gelf_message(json: &str) -> Result<GelfMessage, String> {
    let parsed = json::parse(json).map_err(|err| err.to_string())?;
    let host = parsed["host"].as_str().ok_or("Missing/invalid string field: host")?;
    let short_message = parsed["short_message"].as_str().ok_or("Missing/invalid string field: short_message")?;
    let epoch_ms = parsed["timestamp"].as_fixed_point_u64(3).unwrap_or_else(|| {
        SystemTime::now()
            .duration_since(SystemTime::UNIX_EPOCH)
            .map(|x| x.as_millis())
            .unwrap_or(0) as u64
    });
    let container_name = parsed["_container_name"].as_str().unwrap_or("");

    return Ok(GelfMessage {
        host: host.to_string(),
        short_message: short_message.to_string(),
        epoch_ms,
        container_name: container_name.to_string(),
    });
}

pub fn listen(addr: &str) -> Result<Receiver<GelfMessage>, Box<dyn Error>> {
    let udp_socket = UdpSocket::bind(addr)?;
    udp_socket.set_read_timeout(Some(Duration::from_secs(1)))?;
    let (sender, receiver) = mpsc::channel();
    let mut buf = [0; 65_536];

    thread::spawn(move || loop {
        let count = match udp_socket.recv_from(&mut buf) {
            Ok((res, _)) => res,
            Err(err) => match err.kind() {
                ErrorKind::TimedOut => continue,
                ErrorKind::WouldBlock => continue,
                _ => { log::error!("{:?}", err); continue; }
            }
        };
        
        let json = match from_utf8(&buf[..count]) {
            Ok(res) => res,
            Err(err) => { log::error!("{:?}", err); continue; }
        };
        
        let gelf_message = match parse_gelf_message(json) {
            Ok(res) => res,
            Err(err) => { log::error!("{:?}", err); continue; }
        };

        if let Err(err) = sender.send(gelf_message) {
            log::error!("{:?}", err);
        }
    });
    
    Ok(receiver)
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_parse_gelf() {
        assert_eq!(
            parse_gelf_message("").unwrap_err(),
            "Unexpected end of JSON"
        );
        assert_eq!(
            parse_gelf_message(r#"{}"#).unwrap_err(),
            "Missing/invalid string field: host"
        );
        assert_eq!(
            parse_gelf_message(r#"{ "host": null }"#).unwrap_err(),
            "Missing/invalid string field: host"
        );
        assert_eq!(
            parse_gelf_message(r#"{ "host": 123 }"#).unwrap_err(),
            "Missing/invalid string field: host"
        );
        assert_eq!(
            parse_gelf_message(r#"{ "host": "localhost" }"#).unwrap_err(),
            "Missing/invalid string field: short_message"
        );
        assert_eq!(
            parse_gelf_message(r#"{ "host": "localhost", "short_message": null }"#).unwrap_err(),
            "Missing/invalid string field: short_message"
        );
        assert_eq!(
            parse_gelf_message(r#"{ "host": "localhost", "short_message": 123 }"#).unwrap_err(),
            "Missing/invalid string field: short_message"
        );

        let message = parse_gelf_message(r#"{
            "host": "localhost",
            "short_message": "meg",
            "timestamp": 1620579839.123,
            "_container_name": "container"
        }"#).unwrap();
        assert_eq!(message.host, "localhost");
        assert_eq!(message.short_message, "meg");
        assert_eq!(message.epoch_ms, 1620579839123);
        assert_eq!(message.container_name, "container");
    }

    #[test]
    fn test_json() {
        let parsed = json::parse(r#"{
            "aString": "value",
            "aBool": true,
            "code": 200,
            "success": true,
            "payload": {
                "features": [
                    "awesome",
                    "easyAPI",
                    "lowLearningCurve"
                ]
            }
        }"#).unwrap();

        assert!(parsed.has_key("aString"));
        assert_eq!(parsed["aString"].as_str(), Some("value"));
        assert_eq!(parsed["aString"], "value");

        assert!(parsed.has_key("aBool"));
        assert_eq!(parsed["aBool"].as_bool(), Some(true));
        assert_eq!(parsed["aBool"].as_str(), None);
        assert_eq!(parsed["aBool"], true);

        assert_eq!(parsed.has_key("nonExists"), false);
        assert_eq!(parsed["nonExists"].as_str(), None);
        assert_eq!(parsed["nonExists"], json::Null);

        assert_eq!(parsed["success"].as_str(), None);
        assert_eq!(parsed["success"].as_bool(), Some(true));
        assert_eq!(parsed["success"], true);

        let instantiated = json::object! {
            aString: "value",
            aBool: true,
            code: 200,
            success: true,
            payload: {
                features: [
                    "awesome",
                    "easyAPI",
                    "lowLearningCurve"
                ]
            }
        };

        assert_eq!(parsed, instantiated);
    }
}
