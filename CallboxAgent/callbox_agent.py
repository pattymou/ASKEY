from controller import CallboxController
import argparse
import json

controller = CallboxController(".")

def main():

    parser = argparse.ArgumentParser()
    sub = parser.add_subparsers(dest="cmd")

    p = sub.add_parser("set-band")
    p.add_argument("--cell", type=int, required=True)
    p.add_argument("--band", type=int, required=True)
    p.add_argument("--bandwidth", type=float, required=True)

    args = parser.parse_args()

    if args.cmd == "set-band":

        result = controller.apply_lte(
            args.cell,
            args.band,
            args.bandwidth
        )

        print(json.dumps(result, indent=2))


if __name__ == "__main__":
    main()