import java.util
import org.eclipse.jetty.server.Server
import org.scalatest.junit.JUnitRunner
import org.scalatest.mock.MockitoSugar
import org.scalatest.{Spec, FunSpec}
import util.EmptyStackException

class StackSpec extends FunSpec {
	describe("a stack") {
		it("should pop values in last-in-first-out order") {
			val stack = new util.Stack[Int]
			stack.push(1)
			stack.push(2)
			assert(stack.pop() === 2)
			assert(stack.pop() === 2)
		}

		it("should throw NoSuchElementException if an empty stack is popped") {
			val emptyStack = new util.Stack[Int]
			intercept[EmptyStackException] {
				emptyStack.pop()
			}
		}

		it("xxx") {

		}
	}
}
