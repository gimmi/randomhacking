module.exports = promisify

function promisify(cpsFn) {
  const args = Array.prototype.slice.call(arguments, 1)
  return new Promise((resolve, reject) => cpsFn.apply(null, [...args, (err, result) => {
    if (err) {
      reject(err)
    } else {
      resolve(result)
    }
  }]))
}
