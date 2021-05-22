use cancellation::{CancellationToken, CancellationTokenSource, OperationCanceled};
use std::sync::Arc;
use std::time::Duration;
use std::sync::mpsc;
use std::thread;
use std::sync::mpsc::Receiver;
use crate::gelf::GelfMessage;
use std::thread::JoinHandle;

mod gelf;

fn main() {
    env_logger::init();

    /*    env_logger::builder()
            .format_timestamp(None)
            .target(env_logger::Target::Stdout)
            .write_style(env_logger::WriteStyle::Always)
            .init();
    */

    println!("Run with env var RUST_LOG=trace to see all log messages");

    log::error!("This is an error log");
    log::warn!("This is an warn log");
    log::info!("This is an info log");
    log::debug!("This is an debug log");
    log::trace!("This is an trace log");

    let (sender, receiver) = mpsc::channel();

    let cts = CancellationTokenSource::new();

    let producer_thread = gelf::spawn_producer("127.0.0.1:8080", sender, cts.token().clone()).unwrap();
    
    let consumer_thread = spawn_consumer(receiver, cts.token().clone());

    ctrlc::set_handler(move || {
        log::debug!("Ctrl-C detected");
        cts.cancel();
    }).unwrap();

    log::info!("Running, waiting for Ctrl-C ...");

    producer_thread.join().unwrap();
    consumer_thread.join().unwrap();

    log::info!("Done");
}

fn spawn_consumer(receiver: Receiver<GelfMessage>, ct: Arc<CancellationToken>) -> JoinHandle<()> {
    thread::spawn(move || while !ct.is_canceled() {
        match receiver.recv_timeout(Duration::from_secs(1)) {
            Ok(msg) => log::debug!("Recv1: {:?}", msg),
            Err(mpsc::RecvTimeoutError::Timeout) => continue,
            Err(mpsc::RecvTimeoutError::Disconnected) => break
        }
    })
}
