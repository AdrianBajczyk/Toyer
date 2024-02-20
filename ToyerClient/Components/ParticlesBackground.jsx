import Particles from "react-tsparticles";
import { loadSlim } from "tsparticles-slim";
import { useCallback } from "react";

const ParticlesBackground = () => {

    const particlesInit = useCallback(async (engine) => {
        await loadSlim(engine);
      }, []);
    
  return (
    <Particles
        id="tsparticles"
        init={particlesInit}
        options={{
          autoPlay: true,
          fpsLimit: 120,
          particles: {
            color: {
              value: "#4363ff",
            },
            links: {
              color: "#4363ff",
              distance: 90,
              enable: true,
              opacity: 0.3,
              width: 1,
            },
            move: {
              direction: "right",
              enable: true,
              outModes: {
                default: "out",
              },
              random: false,
              speed: 0.1,
              straight: false,
            },
            number: {
              density: {
                enable: true,
                area: 800,
              },
              value: 120,
            },
            opacity: {
              value: 0.1,
            },
            shape: {
              type: "circle",
            },
            size: {
              value: { min: 1, max: 5 },
            },
          },
          detectRetina: true,
        }}
      />
  )
}

export default ParticlesBackground