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
   
    let rx1 = gelf::listen("127.0.0.1:8080").unwrap();

    loop {
        let msg = rx1.recv().unwrap();

        log::debug!("Recv1: {:?}", msg);
    }
}
