

mergeInto(LibraryManager.library, {
    PlayMIDINote: function (note) {
        // 请求 MIDI 访问权限
        navigator.requestMIDIAccess().then(midiAccess => {
            console.log(midiAccess)

            if (midiAccess != null) {
                console.log("13")

                // 发送 MIDI 输出消息
                var output = midiAccess.outputs.values().next().value;
                output.send(0x007F3C90);
                console.log("23")
            }
        })

        // if (midiAccess != null) {
        //     console.log("13")

        //     // 创建一个新的 MIDI 输出消息
        //     var message = new MIDIMessage(0x90, 0x3C, 0x40); // 0x90: note on, 0x3C: note, 0x40: velocity
        //     message.note = note;
        //     message.velocity = 100;

        //     // 发送 MIDI 输出消息
        //     var output = midiAccess.outputs.values().next().value;
        //     output.send(message);
        //     console.log("23")
        //     console.log(message, midiAccess)
        // }
    },
    StopMIDINote: function (note) {

        var midiAccess;

        // 请求 MIDI 访问权限
        navigator.requestMIDIAccess().then(access => {
            midiAccess = access
        });
        if (midiAccess != null) {
            // 创建一个新的 MIDI 输出消息
            var message = new MIDIMessage(0x80, 0x3C, 0x00); // 0x80: note off, 0x3C: note
            message.note = note

            // 发送 MIDI 输出消息
            var output = midiAccess.outputs.values().next().value
            output.send(message)
        }
    },
    PreImport: function () {
        import("https://unpkg.com/tone@15.0.4/build/Tone.js").then(() => {
            sampler_salamander = new Tone.Sampler({
                urls: {
                    A1: "A1.mp3",
                    A2: "A2.mp3",
                    C3: "C3.mp3"
                },
                baseUrl: "https://tonejs.github.io/audio/salamander/",
                onload: () => {
                    console.log("sampler salamander loaded!")
                }
            })
            sampler_casio = new Tone.Sampler({
                urls: {
                    A1: "A1.mp3",
                    A2: "A2.mp3"
                },
                baseUrl: "https://tonejs.github.io/audio/casio/",
                onload: () => {
                    console.log("sampler casio loaded!")
                }
            })
            sampler = sampler_salamander
        })
    },
    SwitchInstrument: function (instrument) {
        instrument = UTF8ToString(instrument)
        switch (instrument) {
            case "salamander":
                sampler = sampler_salamander
                break
            case "casio":
                sampler = sampler_casio
                break
            default:
                sampler = sampler_salamander
                break
        }

    },
    PlayNote: function (noteName) {
        noteName = UTF8ToString(noteName)
        sampler.triggerAttack(noteName).toDestination()
    },
    StopPlay: function (noteName) {
        noteName = UTF8ToString(noteName)
        sampler.triggerRelease(noteName).toDestination()
    }
});